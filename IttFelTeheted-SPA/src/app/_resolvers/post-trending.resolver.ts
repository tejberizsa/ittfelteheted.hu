import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Post } from '../_models/post';
import { PostService } from '../_services/post.service';

@Injectable()
export class PostTrendingResolver implements Resolve<Post[]> {
    constructor(private postService: PostService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Post[]> {
        return this.postService.getTrendingPosts(route.params['id']).pipe(
            catchError(error => {
                this.alertify.error('Probléma történt az adatok betöltése közben');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
