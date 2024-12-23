import { Component, OnInit } from '@angular/core';
import { UrlService } from '../services/url.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { ShortUrlComponent } from "../short-url/short-url.component";
import { Url } from '../models/url.model';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-short-url-table',
  templateUrl: './short-url-table.component.html',
  styleUrls: ['./short-url-table.component.css'],
  standalone: true,
  imports: [CommonModule, ShortUrlComponent]
})
export class ShortUrlTableComponent implements OnInit {
  urls: Url[] = [];
  
  constructor(public authService: AuthService, private urlService: UrlService, private router: Router) {}
  
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
    this.urlService.deleteUrl(id).subscribe(s => this.loadUrls(), error => error);
  }

  openUrlInfo(id: string) {
    this.router.navigate(["short-url-info", id])
  }
}
