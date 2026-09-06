import { Component, inject, OnInit, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TodoItem } from '../../models/todo-item';
import { TodoService } from '../../services/todo';

import { DateHelper } from '../../helpers/date-helper';

@Component({
  selector: 'app-todo-list',
  imports: [
    DatePipe,
    FormsModule
  ],
  templateUrl: './todo-list.html',
  styleUrl: './todo-list.css',
})
/**
 * TodoList class that demos add, update, delete and patch calls
 */
export class TodoList implements OnInit {

  private readonly todoService = inject(TodoService);

  // latest angular works better with signal approach or we have to use Change detector and 
  // Manually mark for change

  // main todo list
  todos = signal<TodoItem[]>([]);

  //show Add / Edit modal dialog
  showModal = signal(false);
  // Identify if you are in edit mode
  isEditMode = signal(false);

  // Modal message related variables 
  messageModal = signal(false);
  messageTitle = signal('');
  messageText = signal('');
  messageType = signal<'error' | 'success' | 'warning'>('error');
  messageButton = signal<'OK' | 'YesNo'>('OK');
  // Todo item object requested to be deletd, to be used if user says yes to confirmation message
  todoToDelete: TodoItem | null = null;

  // Id of Todo Item being updated
  editingId: number | null = null;

  // Add / Edit Form items
  form = {
    title: '',
    description: '',
    dueDate: '',
    isComplete: false
  };

  // On Init get all todo items
  ngOnInit(): void {
    this.loadTodos();
  }

  // Message Modal html based messaging instead of Alert / Confirm
  showMessage(
    title: string,
    message: string,
    type: 'error' | 'success' | 'warning' = 'error',
    btn: 'OK' | 'YesNo' = 'OK'
  ): void {

    this.messageTitle.set(title);
    this.messageText.set(message);
    this.messageType.set(type);
    this.messageModal.set(true);
    this.messageButton.set(btn);
  }

  // NO button click handler
  noClicked(): void {
    this.messageModal.set(false);
    this.todoToDelete = null;
  }

  // Yes button click handler
  yesClicked(): void {
    this.messageModal.set(false);
    if (this.todoToDelete) {
      this.deleteTodoConfirmed(this.todoToDelete);
    }
    this.todoToDelete = null;
  }

  // Close message modal dialog
  closeMessage(): void {
    this.messageModal.set(false);
  }

  // gets all todo items
  loadTodos(): void {

    this.todoService.getTodos().subscribe({

      next: (response) => {

        console.log('API response:', response);

        this.todos.set(response.data);

        console.log('Todos:', this.todos());
      },

      error: (error) => {
        console.error('Todo API error:', error);
      }

    });
  }


  // Shows Add Dialog 
  openAdd(): void {

    this.isEditMode.set(false);
    this.editingId = null;

    this.form = {
      title: '',
      description: '',
      dueDate: '',
      isComplete: false
    };

    this.showModal.set(true);
  }


  // shows Edit Dialog
  openEdit(todo: TodoItem): void {

    this.isEditMode.set(true);
    this.editingId = todo.id;

    this.form = {
      title: todo.title,
      description: todo.description ?? '',
      dueDate: todo.dueDate
        ? DateHelper.formatDateForInput(todo.dueDate)
        : '',
      isComplete: todo.isComplete
    };

    this.showModal.set(true);
  }


  // Close Add / Edit Dialog
  closeModal(): void {

    this.showModal.set(false);

    this.editingId = null;

    this.form = {
      title: '',
      description: '',
      dueDate: '',
      isComplete: false
    };
  }


  // Save the Add / Edit Form and performs basic validation
  saveTodo(): void {

    if (!this.form.title.trim()) {
      this.showMessage(
        'Validation Error',
        'Title is required.',
        'warning'
      );
      return;
    }
    if (!this.form.description.trim()) {
      this.showMessage(
        'Validation Error',
        'Description is required.',
        'warning'
      );
      return;
    }

    if (this.isEditMode()) {

      this.updateTodo();

    } else {

      this.addTodo();

    }
  }


  // Calls Add todo api
  addTodo(): void {

    const request = {
      title: this.form.title.trim(),
      description: this.form.description.trim(),
      dueDate: this.form.dueDate
        ? new Date(this.form.dueDate).toISOString()
        : null
    };

    this.todoService.addTodo(request).subscribe({

      next: response => {

        console.log('Todo added:', response);


        this.closeModal();
        this.loadTodos();

        this.showMessage(
          'Success',
          'Todo item created successfully.',
          'success'
        );


      },

      error: error => {

        console.error(
          'Error adding todo',
          error
        );

        this.showMessage(
          'Unable to Add Todo',
          error.error?.message ?? 'An unexpected error occurred.',
          'error'
        );

      }

    });
  }


  // Calls Update Todo api 
  updateTodo(): void {

    if (this.editingId === null) {
      return;
    }

    const request = {
      id: this.editingId,
      title: this.form.title.trim(),
      description: this.form.description.trim(),
      dueDate: this.form.dueDate
        ? new Date(this.form.dueDate).toISOString()
        : null,
      isComplete: this.form.isComplete
    };

    this.todoService
      .updateTodo(this.editingId, request)
      .subscribe({

        next: response => {

          console.log(
            'Todo updated:',
            response
          );



          this.closeModal();
          this.loadTodos();


          this.showMessage(
            'Success',
            'Todo item updated successfully.',
            'success'
          );

        },

        error: error => {

          console.error(
            'Error updating todo',
            error
          );

          this.showMessage(
            'Unable to Update Todo',
            error.error?.message ?? 'An unexpected error occurred.',
            'error'
          );

        }

      });
  }


  // Calls Delete Todo api after confirmation
  deleteTodoConfirmed(todo: TodoItem): void {

    this.todoService
      .deleteTodo(todo.id)
      .subscribe({

        next: () => {

          // clear the item or call the intial load again
          this.todos.update(items =>
            items.filter(x => x.id !== todo.id)
          );

        },

        error: error => {

          console.error(
            'Error deleting todo',
            error
          );

        }

      });

  }

  // Delete Todo action handler
  deleteTodo(todo: TodoItem): void {


    this.todoToDelete = todo;
    let msg = `Are you sure you want to delete - <strong>${todo.title}</strong> ?`;

    this.showMessage(
      'Confirmation',
      msg,
      'warning', 'YesNo'
    );



  }


  // Toggle Status hanlder to mark complete or pending
  toggleComplete(todo: TodoItem): void {

    const newStatus = !todo.isComplete;

    this.todoService
      .markComplete(todo.id, newStatus)
      .subscribe({

        next: response => {

          if (response.data) {

            this.loadTodos();

            this.showMessage(
              'Success',
              'Todo item updated successfully.',
              'success'
            );

          } else {

            console.log(
              'Unable to update status'
            );
            this.showMessage(
              'Error',
              'Unable to update status.',
              'error'
            );

          }

        },

        error: error => {

          console.error(
            'Error updating todo status',
            error
          );

          this.showMessage(
            'Error',
            'Error updating todo status.',
            'error'
          );

        }

      });
  }



}

