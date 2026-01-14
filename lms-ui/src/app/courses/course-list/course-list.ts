import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Course, CourseService } from '../../services/course.service';
import { CourseFormComponent } from '../course-form/course-form';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [CommonModule, FormsModule, CourseFormComponent],
  templateUrl: './course-list.html',
  styleUrls: ['./course-list.css'],
})
export class CourseListComponent implements OnInit {
  courses: Course[] = [];
  editingCourse: Course | null = null;

  // 🔍 Search text
  searchText: string = '';

  constructor(private courseService: CourseService) {}

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses() {
    this.courseService.getCourses().subscribe({
      next: (data) => (this.courses = data),
      error: (err) => console.error(err),
    });
  }

  // 🔍 Filter logic
  get filteredCourses(): Course[] {
    if (!this.searchText.trim()) {
      return this.courses;
    }

    const text = this.searchText.toLowerCase();

    return this.courses.filter(
      (c) => c.title.toLowerCase().includes(text) || c.description.toLowerCase().includes(text)
    );
  }

  startEdit(course: Course) {
    this.editingCourse = { ...course };
  }

  cancelEdit() {
    this.editingCourse = null;
  }

  updateCourse() {
    if (!this.editingCourse?.id) return;

    this.courseService.updateCourse(this.editingCourse.id, this.editingCourse).subscribe(() => {
      this.editingCourse = null;
      this.loadCourses();
    });
  }

  deleteCourse(id: number) {
    this.courseService.deleteCourse(id).subscribe(() => {
      this.loadCourses();
    });
  }
}
