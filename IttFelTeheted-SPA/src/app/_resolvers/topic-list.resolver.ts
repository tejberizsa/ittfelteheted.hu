import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PostService } from '../_services/post.service';
import { Topic } from '../_models/topic';

@Injectable()
export class TopicListResolver implements Resolve<Topic[]> {
    constructor(private postService: PostService, private router: Router, private alertify: AlertifyService) {}

    resolve(route: ActivatedRouteSnapshot): Observable<Topic[]> {
        return this.postService.getTopics().pipe(
            catchError(error => {
                this.alertify.error('Probléma történt az adatok betöltése közben');
                this.router.navigate(['/home']);
                return of(null);
            })
        );
    }
}
