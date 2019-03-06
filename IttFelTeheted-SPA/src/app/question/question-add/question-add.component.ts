import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/_models/post';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { PostService } from 'src/app/_services/post.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Topic } from 'src/app/_models/topic';
import { Photo } from 'src/app/_models/photo';

@Component({
  selector: 'app-question-add',
  templateUrl: './question-add.component.html',
  styleUrls: ['./question-add.component.css']
})
export class QuestionAddComponent implements OnInit {
  editorConf: any;
  post: Post;
  postId: number;
  postForm: FormGroup;
  baseUrl = environment.apiUrl;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;
  topics: Topic[];

  constructor(private fb: FormBuilder, private authService: AuthService, private route: ActivatedRoute,
      private alertify: AlertifyService, private postService: PostService, private router: Router) { }

  ngOnInit() {
    this.createPostForm();
    this.route.data.subscribe(data => {
      this.topics = data['topics'];
    });

    this.editorConf = {
      editable: true,
      spellcheck: true,
      height: 'auto',
      minHeight: 340,
      width: 'auto',
      minWidth: '0',
      translate: 'no',
      enableToolbar: true,
      showToolbar: true,
      placeholder: 'Válaszodat ide írhatod...',
      imageEndPoint: '',
      toolbar: [
          ['bold', 'italic', 'underline', 'strikeThrough', 'superscript', 'subscript'],
          ['fontName', 'fontSize', 'color'],
          ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull', 'indent', 'outdent'],
          ['cut', 'copy', 'delete', 'removeFormat', 'undo', 'redo'],
          ['paragraph', 'blockquote', 'removeBlockquote', 'horizontalLine', 'orderedList', 'unorderedList']
      ]
    };
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  createPostForm() {
    this.postForm = this.fb.group({
      title: ['', Validators.required],
      postBody: ['', Validators.max(4000)],
      topicId: ['', Validators.required]
    }
    );
  }

  sendPost() {
    if (!this.authService.loggedIn()) {
      this.alertify.error('Kérdés küldéshez be kell jelentkezned');
      return;
    }
    if (this.postForm.valid) {
      this.post = Object.assign({}, this.postForm.value);
      this.post.userId = this.authService.decodedToken.nameid;
      this.postService.sendPost(this.post).subscribe((postReturn: Post) => {
      this.postId = postReturn.id;
      this.initializeUploader();
      this.uploader.uploadAll();
      this.alertify.success('Kérdésed, posztod megjelent az oldalon');
      }, error => {
        this.alertify.error(error);
      });
    }
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'posts/' + this.postId + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        if (this.uploader.queue.length < 1) {
          this.router.navigate(['/detail/', this.postId]);
        }
      }
    };
  }

}
