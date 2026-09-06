import { Routes } from '@angular/router';
import { TodoList } from './pages/todo-list/todo-list';

// define routes here for future when new pages are added
export const routes: Routes = [

    {path:'',component:TodoList},
    {path:'**', redirectTo:''}


];
