import { Component, Input, OnInit } from '@angular/core';
import { UrlService } from '../services/url.service';
import { DetailedUrl } from '../models/detailed-url.model';

@Component({
  selector: 'app-short-url-info',
  templateUrl: './short-url-info.component.html',
  styleUrls: ['./short-url-info.component.css'],
})
export class ShortUrlInfoComponent implements OnInit {
  @Input() urlId!: string;
  detailedUrl!: DetailedUrl;

  constructor(private urlService: UrlService) {}

  ngOnInit(): void {
    this.loadUrlDetails();
  }

  loadUrlDetails() {
    if (this.urlId) {
      this.urlService.getUrlById(this.urlId).subscribe(
        (data) => {
          this.detailedUrl = data;
        },
        (error) => {
          console.error('Failed to fetch URL details', error);
        }
      );
    }
  }
}
