import { Component, OnInit } from '@angular/core';
import { Post } from 'src/app/_models/post';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';
import { AuthService } from 'src/app/_services/auth.service';
import { Answer } from 'src/app/_models/answer';
import { PostService } from 'src/app/_services/post.service';
import { UserService } from 'src/app/_services/user.service';

@Component({
  selector: 'app-question-detail',
  templateUrl: './questionDetail.component.html',
  styleUrls: ['./questionDetail.component.css']
})
export class QuestionDetailComponent implements OnInit {
  trendings: Post[];
  post: Post;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  newAnswer: any = {};
  editorConfig: any;

  constructor(private postService: PostService, private alertify: AlertifyService,
    private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.post = data['post'];
      this.post.answers.sort((a, b) => b.like - a.like);
      this.trendings = data['trendings'];
    });

    this.galleryOptions = [
      {
        width: '100%',
        height: '600px',
        imagePercent: 100,
        thumbnails: false,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: false
      }
    ];
    this.galleryImages = this.getImages();

    this.editorConfig = {
      editable: true,
      spellcheck: true,
      height: 'auto',
      minHeight: 200,
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

  getImages() {
    const imageUrls = [];
    for (let i = 0; i < this.post.photos.length; i++) {
      imageUrls.push({
        small: this.post.photos[i].url,
        medium: this.post.photos[i].url,
        big: this.post.photos[i].url
      });
    }
    return imageUrls;
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  mustLogin() {
    this.alertify.error('Előbb be kell jelentkezned');
  }

  sendAnswer() {
    this.newAnswer.userId = this.authService.decodedToken.nameid;
    this.postService.sendAnswer(this.post.id, this.newAnswer).subscribe((answer: Answer) => {
      this.post.answers.unshift(answer);
      this.newAnswer = {};
    }, error => {
      this.alertify.error(error);
    });
  }

}
