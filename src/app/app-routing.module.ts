import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminPostsComponent } from './admin/admin-posts/admin-posts.component';
import { AdminViwPostComponent } from './admin/admin-viw-post/admin-viw-post.component';
import { AdminAddPostComponent } from './admin/admin-add-post/admin-add-post.component';
import { PostsComponent } from './posts/posts.component';
import { PostComponent } from './post/post.component';

const routes: Routes = [
  {
    path:'', 
    component: PostsComponent
  }, 
  {
    path: 'post/:id', // Updated path for individual blog post
    component: PostComponent // Use the PostComponent for displaying individual posts
  },
  {
    path:'admin/Posts', 
    component: AdminPostsComponent
  },
  {
    path:'admin/Posts/add', 
    component: AdminAddPostComponent
  },
  {
    path:'admin/Posts/:id', 
    component: AdminViwPostComponent
  },
  // Add the new routes for view and add here
  {
    path: 'view',
    component: PostComponent, // Replace with the appropriate component for viewing
  },
  {
    path: 'add',
    component: AdminAddPostComponent, // Replace with the appropriate component for adding
  },

];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
