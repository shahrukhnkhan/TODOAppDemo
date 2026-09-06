// DTO for Patching Todo Item 
export interface TodoPatch {
  title?: string;
  description?: string;
  isComplete?: boolean;
  dueDate?: string|null;
}