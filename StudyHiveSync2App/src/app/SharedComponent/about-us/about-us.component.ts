import { Component } from '@angular/core';

@Component({
  selector: 'app-about-us',
  imports: [],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.css'
})
export class AboutUsComponent {
  learningGridArray = [
    {
      order: -1,
      heading: "World-Class Learning for",
      highlightText: "Anyone, Anywhere",
      description: "Studynotion partners with more than 275+ leading universities and companies to bring flexible, affordable, job-relevant online learning to individuals and organizations worldwide.",
      BtnText: "Learn More",
      BtnLink: "/",
    },
    {
      order: 1,
      heading: "Curriculum Based on Industry Needs",
      description: "Save time and money! The Belajar curriculum is made to be easier to understand and in line with industry needs.",
    },
    {
      order: 2,
      heading: "Our Learning Methods",
      description: "Studynotion partners with more than 275+ leading universities and companies to bring",
    },
    {
      order: 3,
      heading: "Certification",
      description: "Studynotion partners with more than 275+ leading universities and companies to bring",
    },
    {
      order: 4,
      heading: `Rating "Auto-grading"`,
      description: "Studynotion partners with more than 275+ leading universities and companies to bring",
    },
    {
      order: 5,
      heading: "Ready to Work",
      description: "Studynotion partners with more than 275+ leading universities and companies to bring",
    },
  ];

  getCardClass(card: any, index: number) {
    if (index === 0) {
      return 'xl:col-span-2 xl:h-[294px]';
    } else if (card.order % 2 === 1) {
      return 'bg-richblack-700 h-[294px]';
    } else if (card.order % 2 === 0) {
      return 'bg-richblack-800 h-[294px]';
    } else if (card.order === 3) {
      return 'xl:col-start-2';
    } else {
      return 'bg-transparent';
    }
  }
}
