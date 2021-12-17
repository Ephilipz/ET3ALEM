import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-message-dialog',
  templateUrl: './message-dialog.component.html',
  styleUrls: ['./message-dialog.component.css']
})
export class MessageDialogComponent implements OnInit {

  subject = '';
  message = '';
  constructor(@Inject(MAT_DIALOG_DATA) public data: { subject: string, message: string }) {
    this.subject = this.data.subject;
    this.message = this.data.message;
  }

  ngOnInit(): void {
  }
}
