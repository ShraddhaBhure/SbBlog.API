import { Component, OnInit } from '@angular/core';
import { PostService } from '../services/post.service';
import { Post } from '../models/post.model';
import { Router } from '@angular/router'; // Import the Router module

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  constructor(private postService: PostService, private router: Router) {}

  posts: Post[] = [];

  ngOnInit(): void {
    this.postService.getAllPosts().subscribe(
      response => {
        this.posts = response;
      }
    );
  }

  viewPost(id: string) {
    this.router.navigate(['/post', id]);
  }
}
