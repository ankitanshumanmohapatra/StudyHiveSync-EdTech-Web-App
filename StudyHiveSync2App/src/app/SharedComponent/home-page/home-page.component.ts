import { Component } from '@angular/core';

@Component({
  selector: 'app-home-page',
  imports: [],
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
  constructor() { }

  ngOnInit(): void {
  }

  onSignUpClick(): void {
    alert('Redirect to Sign Up'); // Replace with actual navigation logic
  }
}
