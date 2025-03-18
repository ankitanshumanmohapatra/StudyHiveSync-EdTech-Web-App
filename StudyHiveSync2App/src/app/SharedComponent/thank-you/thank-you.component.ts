import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-thank-you',
  imports: [],
  templateUrl: './thank-you.component.html',
  styleUrl: './thank-you.component.css'
})
export class ThankYouComponent implements OnInit {
  orderId: string | null = null;
  // paymentId: string | null = null;

  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    // Retrieve orderId and paymentId from query parameters
    this.route.queryParams.subscribe(params => {
      this.orderId = params['orderId'] || 'N/A';
      // this.paymentId = params['paymentId'] || 'N/A';
    });
  }

  // Navigate to a resources page when the "Access Resources" button is clicked
  accessResources(): void {
    // this.router.navigate(['/resources']); // Adjust the route as needed
    window.open('https://drive.google.com/drive/folders/1RZ1F6AkiLID3xDFBmoE6eSOF4buQAnhQ?usp=sharing', '_blank');
  }
}
