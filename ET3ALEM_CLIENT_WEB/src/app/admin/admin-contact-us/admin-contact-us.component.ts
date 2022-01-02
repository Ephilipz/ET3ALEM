import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ContactService } from 'src/app/Shared/services/contact.service';
import { MessageDialogComponent } from '../message-dialog/message-dialog.component';

@Component({
  selector: 'app-admin-contact-us',
  templateUrl: './admin-contact-us.component.html',
  styleUrls: ['./admin-contact-us.component.css']
})
export class AdminContactUsComponent implements OnInit {

  messagesListDs = new MatTableDataSource();
  displayedColumns: string[] = ['Email', 'Subject', 'Message'];
  constructor(private contactService: ContactService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadContactUsMessages();
  }
  loadContactUsMessages() {
    this.contactService.getContactUsMessages().subscribe(
      res => {
        this.messagesListDs.data = res;
      }
    );
  }
  openDialog(subject: string, message: string) {
    this.dialog.open(MessageDialogComponent, {
      minHeight: '400px',
      minWidth: '600px',
      data: {
        subject,
        message
      },
    });
  }
  formatMessage(message: string): string {
    if (message.length > 8) {
      return message.substring(0, 8) + '...';
    }
    return message;
  }

}
