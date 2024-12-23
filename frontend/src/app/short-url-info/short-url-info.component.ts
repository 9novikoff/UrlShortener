import { Component, Input, OnInit } from '@angular/core';
import { UrlService } from '../services/url.service';
import { DetailedUrl } from '../models/detailed-url.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-short-url-info',
  templateUrl: './short-url-info.component.html',
  styleUrls: ['./short-url-info.component.css'],
})
export class ShortUrlInfoComponent implements OnInit {
  detailedUrl!: DetailedUrl;

  constructor(private urlService: UrlService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    const urlId = this.route.snapshot.paramMap.get('id');
    this.loadUrlDetails(urlId!);
  }

  loadUrlDetails(id: string) {
    if (id) {
      this.urlService.getUrlById(id).subscribe(
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
