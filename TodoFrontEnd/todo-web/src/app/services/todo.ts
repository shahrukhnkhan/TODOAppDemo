import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoItem } from '../models/todo-item';
import { ApiResponse } from '../models/api-response';

import { TodoCreate } from '../models/todo-create';
import { TodoUpdate } from '../models/todo-update';
import { TodoPatch } from '../models/todo-patch';


@Service()
export class TodoService {

    private readonly http = inject(HttpClient);

    private readonly apiUrl = 'http://localhost:5254/api/Todo'; // Define in Env Later


    // Get All Todo Items
    getTodos(): Observable<ApiResponse<TodoItem[]>> {
        return this.http.get<ApiResponse<TodoItem[]>>(this.apiUrl);
    }

    // Get Todo Item by Id 
    getTodoById(id: number): Observable<ApiResponse<TodoItem>> {
        return this.http.get<ApiResponse<TodoItem>>(
            `${this.apiUrl}/${id}`
        );
    }
    

    // Add new Todo Item
    addTodo(item: TodoCreate): Observable<ApiResponse<TodoItem>> {
        return this.http.post<ApiResponse<TodoItem>>(
            this.apiUrl,
            item
        );
    }

    // Update existing Todo Item
    updateTodo(
        id: number,
        item: TodoUpdate
    ): Observable<ApiResponse<TodoItem>> {

        return this.http.put<ApiResponse<TodoItem>>(
            `${this.apiUrl}/${id}`,
            item
        );
    }

    // Patch Todo Item
    patchTodo(
        id: number,
        item: TodoPatch
    ): Observable<ApiResponse<TodoItem>> {

        return this.http.patch<ApiResponse<TodoItem>>(
            `${this.apiUrl}/${id}`,
            item
        );
    }

    // Delete Todo Item
    deleteTodo(id: number): Observable<ApiResponse<boolean>> {
        return this.http.delete<ApiResponse<boolean>>(
            `${this.apiUrl}/${id}`
        );
    }

    // Toggle Copmletion status internally calls patch api
    markComplete(
        id: number,
        isComplete: boolean
    ): Observable<ApiResponse<TodoItem>> {

        return this.patchTodo(id, {
            isComplete: isComplete
        });
    }

}
