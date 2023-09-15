import { Component, OnInit } from '@angular/core';
import { AddPostRequest } from 'src/app/models/add-post.model';
import { PostService } from 'src/app/services/post.service';
import { Post } from 'src/app/models/post.model';


@Component({
  selector: 'app-admin-add-post',
  templateUrl: './admin-add-post.component.html',
  styleUrls: ['./admin-add-post.component.css']
})
export class AdminAddPostComponent implements OnInit {

  constructor( private postService: PostService) { }

     post: AddPostRequest = {
     author: '',
     content:'',
     title:'',
     summary:'',
     urlHandel:'',
     featuredImageUrl:'',
     visible:true,
     publishDate:'',
     updatedDate:''
}

 ngOnInit(): void {

 }
onSubmit():void{
  this.postService.addPost(this.post).subscribe(
    response =>{
      alert('success');
    });
}
}