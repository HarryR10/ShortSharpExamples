import { Component } from '@angular/core';

@Component({
    selector: 'app',
    template: `<input [(ngModel)]="text" placeholder="text" />
                 <h2>Ok, {{text}}</h2>`
})
export class AppComponent {
    text = '';
}
