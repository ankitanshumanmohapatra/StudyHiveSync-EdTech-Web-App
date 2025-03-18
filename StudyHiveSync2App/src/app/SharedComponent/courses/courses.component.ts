import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
// import { HttpClientModule } from '@angular/common/http';
import { ReviewService } from '../../services/review.service';


@Component({
  selector: 'app-course-card',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css'],
  standalone: true,
  imports: [FormsModule,CommonModule,], // Import required modules
  providers: [ReviewService] // Provide the service directly
})
export class CoursesComponent implements OnInit {
  reviews: any[] = [];
  showForm: boolean = false;
  editMode: boolean = false;
  reviewData: any = { userId: '', rating: 0, review: '', courseId: '' };

  constructor(private reviewService: ReviewService) { }

  ngOnInit(): void {
    this.loadReviews();
  }

  loadReviews(): void {
    this.reviewService.getReviews().subscribe({
      next: (data: any) => {
        if(data && data.$values && Array.isArray(data.$values)) {
          this.reviews = data.$values;

        }
      },
      error: (err) => {
        console.error('Error fetching reviews:', err);
      }
    });
  }

  openReviewForm(): void {
    this.editMode = false;
    this.reviewData = { userId: '', rating: 0, review: '', courseId: '' };
    this.showForm = true;
  }

  editReview(review: any): void {
    this.editMode = true;
    this.reviewData = { ...review };
    this.showForm = true;
  }

  saveReview(): void {
    if (this.editMode) {
      this.reviewService.updateReview(this.reviewData.ratingAndReviewId, this.reviewData).subscribe({
        next: () => {
          this.loadReviews();
          this.closeReviewForm();
        },
        error: (err) => console.error('Error updating review:', err)
      });
    } else {
      this.reviewService.addReview(this.reviewData).subscribe({
        next: () => {
          this.loadReviews();
          this.closeReviewForm();
        },
        error: (err) => console.error('Error adding review:', err)
      });
    }
  }

  deleteReview(id: number): void {
    if (confirm('Are you sure you want to delete this review?')) {
      this.reviewService.deleteReview(id).subscribe({
        next: () => {
          this.loadReviews();
        },
        error: (err) => console.error('Error deleting review:', err)
      });
    }
  }

  closeReviewForm(): void {
    this.showForm = false;
    this.editMode = false;
    this.reviewData = { userId: '', rating: 0, review: '', courseId: '' };
  }

  // getStars(rating: number): string {
  //   const fullStars = Math.floor(rating);
  //   const halfStar = rating % 1 >= 0.5 ? '½' : '';
  //   const emptyStars = 5 - Math.ceil(rating);
  //   return '⭐'.repeat(fullStars) + halfStar + '☆'.repeat(emptyStars);
  // }

  collapseAllSections(event: Event): void {
    event.preventDefault();
    const accordionItems = document.querySelectorAll('.accordion-collapse');
    accordionItems.forEach(item => {
      item.classList.remove('show');
    });

    const accordionButtons = document.querySelectorAll('.accordion-button');
    accordionButtons.forEach(button => {
      button.classList.add('collapsed');
    });
  }
}














// import { Component, OnInit } from '@angular/core';

// @Component({
//   selector: 'app-course-card',
//   templateUrl: './courses.component.html',
//   styleUrls: ['./courses.component.css']
// })
// export class CoursesComponent implements OnInit {
//   constructor() { }

//   ngOnInit(): void { }

//   collapseAllSections(event: Event): void {
//     event.preventDefault(); // Prevent default anchor behavior
//     const accordionItems = document.querySelectorAll('.accordion-collapse');
//     accordionItems.forEach(item => {
//       item.classList.remove('show'); // Collapse all sections
//     });

//     const accordionButtons = document.querySelectorAll('.accordion-button');
//     accordionButtons.forEach(button => {
//       button.classList.add('collapsed'); // Update button state
//     });
//   }
// }




