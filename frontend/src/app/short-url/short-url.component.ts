import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
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
  @Output() urlCreated = new EventEmitter();

  constructor(private urlService: UrlService, private router: Router) {
    
  }
  originalUrl: string = '';
  shortenedUrl: string = '';
  errorMessage: string = '';

  generateShortUrl() {
    this.errorMessage = '';

    if (this.originalUrl) {
      this.urlService.createUrl(this.originalUrl).subscribe(res => 
        {
          this.shortenedUrl = res.shortUrl;
          this.urlCreated.emit()
        }, 
        error => this.errorMessage = error.error);
    } else {
      this.errorMessage = 'Please enter a valid URL!';
    }
  }
}