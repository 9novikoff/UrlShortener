import { Component, OnInit } from '@angular/core';
import { UrlService } from '../services/url.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-short-url-table',
  templateUrl: './short-url-table.component.html',
  styleUrls: ['./short-url-table.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class ShortUrlTableComponent implements OnInit {
  urls: any[] = [];
  
  constructor(private urlService: UrlService, private router: Router) {}
  
  ngOnInit(): void {
    this.loadUrls();
  }
  
  loadUrls() {
    this.urlService.getUrls().subscribe(
      (data) => {
        this.urls = data;
      },
      (error) => {
        console.error('Failed to load URLs', error);
      }
    );
  }

  deleteUrl(id: string) {
    this.urlService.deleteUrl(id);
  }

  openAddUrlForm() {
    this.router.navigate([""])
  }
}
