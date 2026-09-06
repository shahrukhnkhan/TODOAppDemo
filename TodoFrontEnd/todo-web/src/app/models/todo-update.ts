// DTO for Updating Todo Item
export interface TodoUpdate {
  title: string;
  description: string;
  isComplete: boolean;
  dueDate: string|null;
}