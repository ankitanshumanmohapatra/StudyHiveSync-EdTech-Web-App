import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { NavBarComponent } from './SharedComponent/nav-bar/nav-bar.component';
import { CommonModule } from '@angular/common';
import { NgxPayPalModule } from 'ngx-paypal';

@Component({
  selector: 'app-root',
  imports: [NavBarComponent,CommonModule,RouterOutlet,NgxPayPalModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'StudyHiveSyncApp';

  showNavBar = true;

  constructor(private router: Router) { 
    this.router.events.subscribe(() => {
      this.showNavBar = !['/signup', '/login'].includes(this.router.url);
    })
  }
}
