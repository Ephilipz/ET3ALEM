import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ContactService } from 'src/app/Shared/services/contact.service';

@Component({
  selector: 'app-admin-contact-us',
  templateUrl: './admin-contact-us.component.html',
  styleUrls: ['./admin-contact-us.component.css']
})
export class AdminContactUsComponent implements OnInit {

  messagesListDs = new MatTableDataSource();
  displayedColumns: string[] = ['Email', 'Subject', 'Message'];
  constructor(private contactService: ContactService) { }

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

}
