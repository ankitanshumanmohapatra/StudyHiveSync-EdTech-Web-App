import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category',
  imports: [CommonModule],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})

export class CategoryComponent {
  constructor(private router: Router, private http: HttpClient) { }
  programCourses = [
    { id: 1, title: 'Python Programming Bootcamp from Basics', instructor: 'Ankit Mohapatra', rating: 4.8, price: 699, isBestseller: true,photo: 'https://i0.wp.com/junilearning.com/wp-content/uploads/2020/06/python-programming-language.webp?fit=800%2C800&ssl=1' },
    { id: 2,title: 'C# Programming Language Master Course', instructor: 'Abhisekh Rathod', rating: 4.9, price: 1299, isBestseller: false, photo: 'https://i.ytimg.com/vi/kU-t1qsEbqk/sddefault.jpg?v=5f62a832' },
    { id: 3,title: 'C++ Programming from Basics till Advanced', instructor: 'Shubham Negi', rating: 4.2, price: 899, isBestseller: false,photo: 'https://miro.medium.com/v2/resize:fit:1400/0*L6wObMfAdFcUISN2.png' },
  ];

  mlCourses = [
    { title: 'Machine Learning Algorithms & Applications', instructor: 'Rajdeep Kundu', rating: 4.0, price: 399, isBestseller: true,photo: 'https://www.naukri.com/campus/career-guidance/wp-content/uploads/2024/07/what-is-machine-learning.jpg' },
    { title: 'Google Data Analytics using R Programming', instructor: 'Jyotiraditya Nanda', rating: 4.1, price: 999, isBestseller: false, photo: 'https://play-lh.googleusercontent.com/1-hPxafOxdYpYZEOKzNIkSP43HXCNftVJVttoo4ucl7rsMASXW3Xr6GlXURCubE1tA=w3840-h2160-rw' },
    { title: 'Data Science Master Course for Beginners', instructor: 'Ashutosh Yash', rating: 4.7, price: 459, isBestseller: false,photo: 'https://imageio.forbes.com/specials-images/imageserve/635f79fbf214917bd2876e03/0x0.jpg?format=jpg&height=900&width=1600&fit=bounds' },
  ];
  webdevCourses=[
    { title: 'Web Development Bootcamp using HTML & CSS', instructor: 'Pratyush Singh', rating: 4.6, price: 199, isBestseller: true,photo: 'https://t3.ftcdn.net/jpg/02/14/87/96/360_F_214879686_R3HFJlk6WLr1kcdvy6Q9rtNASKN0BZBS.jpg' },
    { title: 'MEAN Stack Angular Master Course', instructor: 'Abhas Nayak', rating: 4.2, price: 1599, isBestseller: false, photo: 'https://s3-ap-south-1.amazonaws.com/trt-blog-ghost/2023/01/Mean-Tech-stack.png' },
    { title: 'Full Stack Development EdTech Project', instructor: 'Bishnu Prasad Sahoo', rating: 4.2, price: 759, isBestseller: false,photo: 'https://media.geeksforgeeks.org/wp-content/cdn-uploads/20190626123927/untitlsssssed.png' },
  ]



  onCourseClick(id: number) {
    this.router.navigate([`/course/${id}`]);
  }
}
