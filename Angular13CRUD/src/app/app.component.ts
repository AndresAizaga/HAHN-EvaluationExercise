import { ApiService } from './shared/services/api.service';
import { MatDialog } from '@angular/material/dialog';
import { Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AddproductdialogComponent } from './addproductdialog/addproductdialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  displayedColumns: string[] = [
    'productName',
    'productCategory',
    'freshness',
    'description',
    'price',
    'comment',
    'date',
    'action'
  ];
  displayedColumns2: string[] = [
    'Name',
    'Email',
    'Phone',
    'Age',
    'action'
  ];
  dataSource!: MatTableDataSource<any>;
  dataSource2!: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  constructor(public dialog: MatDialog, private api: ApiService) {}
  ngOnInit(): void {
    this.getContacts();
  }
  openDialog() {
    let dialogBox = this.dialog.open(AddproductdialogComponent, {
      width: '30%',
    });
    dialogBox.afterClosed().subscribe((val) => {
      if(val==="save"){
        this.getContacts();
      }
    });
  } 
  getContacts(){
    this.api.getContacts().subscribe({
      next : (res) => {
        
        this.dataSource2 = new MatTableDataSource(res);
        console.log("dataSource2: ", this.dataSource2);
      }
    });
  }
  
  editContact(row:any){
  this.dialog.open(AddproductdialogComponent,{
    width:'30%',
    data:row
  }).afterClosed().subscribe(val=>{
    if(val==="update"){
      this.getContacts();
    }
  })
  }

  delete(row:any){
    this.api.deleteContact(row.id)
    .subscribe({
      next:(res)=>{
        alert("deleted Successfully");
        this.getContacts();
      },
      error:(err)=>{
        alert("Error while deleting");
      }
    })
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }



}
