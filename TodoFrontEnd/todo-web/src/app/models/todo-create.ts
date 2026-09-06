// DTO for Creating a Todo Item
export interface TodoCreate {
  title: string;
  description: string;
  dueDate: string|null;
}