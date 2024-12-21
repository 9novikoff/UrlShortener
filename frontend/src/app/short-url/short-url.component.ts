import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UrlService } from '../services/url.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-short-url',
  templateUrl: './short-url.component.html',
  styleUrls: ['./short-url.component.css'],
  standalone: true,
  imports: [FormsModule, CommonModule]

})
export class ShortUrlComponent {
  constructor(private urlService: UrlService, private router: Router) {
    
  }
  originalUrl: string = '';
  shortenedUrl: string = '';

  generateShortUrl() {
    if (this.originalUrl) {
      this.urlService.createUrl(this.originalUrl).subscribe(res => this.shortenedUrl = res);

    } else {
      alert('Please enter a valid URL!');
    }
  }
}
