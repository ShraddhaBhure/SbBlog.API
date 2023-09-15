import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/post.model';
import { UpdatePostRequest } from 'src/app/models/update-post.model';
import { PostService } from 'src/app/services/post.service';

@Component({
  selector: 'app-admin-viw-post',
  templateUrl: './admin-viw-post.component.html',
  styleUrls: ['./admin-viw-post.component.css']
})
export class AdminViwPostComponent implements OnInit {
  constructor(private route: ActivatedRoute, private postService: PostService) {}

  post: Post | undefined;

  ngOnInit(): void {
    this.route.paramMap.subscribe((params) => {
      const id = params.get('id');

      if (id) {
        this.postService.getPostById(id).subscribe((response) => {
          this.post = response;
        });
      }
    });
  }

  onSubmit(): void {
    const updatePostRequest :UpdatePostRequest= {
     author: this.post?.author,
     content:this.post?.content,
     title:this.post?.title,
     summary:this.post?.summary, 
     urlHandel:this.post?.urlHandel, 
     featuredImageUrl:this.post?.featuredImageUrl, 
     visible:this.post?.visible, 
     publishDate:this.post?.publishDate,
     updatedDate:this.post?.updatedDate
  }

  this.postService.updatePost(this.post?.id, updatePostRequest).subscribe(
  response =>{
    alert('success');
  },
    error => {
    console.error('Error:', error);
    alert('Failed to update'); 
    }
  );}
  deletePost(): void {
    this.postService.deletePost( this.post?.id).subscribe(
      response =>{
        alert('Deleted successfully');
      }
    );
  }
}

