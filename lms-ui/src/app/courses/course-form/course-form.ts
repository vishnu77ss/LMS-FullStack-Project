import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CourseService, Course } from '../../services/course.service';

@Component({
  selector: 'app-course-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './course-form.html',
  styleUrls: ['./course-form.css'],
})
export class CourseFormComponent {
  course: Course = {
    title: '',
    description: '',
    durationInHours: 1,
  };

  @Output() courseAdded = new EventEmitter<void>();

  constructor(private courseService: CourseService) {}

  submit(form: any) {
    if (form.invalid) {
      return;
    }

    this.courseService.addCourse(this.course).subscribe({
      next: () => {
        this.course = { title: '', description: '', durationInHours: 1 };
        form.resetForm();
        this.courseAdded.emit();
      },
      error: (err) => console.error(err),
    });
  }
}
