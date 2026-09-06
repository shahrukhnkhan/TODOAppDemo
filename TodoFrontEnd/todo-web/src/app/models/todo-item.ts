// Todo Object that the API return
export interface TodoItem {
  id: number;
  title: string;
  description: string;
  isComplete: boolean;
  creationDate: Date;
  dueDate: Date;

}