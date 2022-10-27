import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ApiService } from './../shared/services/api.service';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-addproductdialog',
  templateUrl: './addproductdialog.component.html',
  styleUrls: ['./addproductdialog.component.scss'],
})
export class AddproductdialogComponent implements OnInit {
  freshnessValue!: string;
  freshnessList: string[] = ['Brand New', 'Second Hand', 'Refurbished'];
  selectedCategory: string = '';
  productForm!: FormGroup;
  actBtn : string="Save"
  constructor(
    private formBuilder: FormBuilder,
    private api: ApiService,
    private matDialog: MatDialogRef<AddproductdialogComponent>,
    @Inject(MAT_DIALOG_DATA) public editData: any
  ) {}

  ngOnInit(): void {
    this.productForm = this.formBuilder.group({
      id: ['', Validators.required],
      name: ['', Validators.required],
      email: ['', Validators.required],
      phone:['',Validators.required],
      age: ['', Validators.required]
    });

    if (this.editData) {
      this.actBtn = "Update"
     
      this.productForm.controls['id'].setValue(
        this.editData.id
      );

      this.productForm.controls['name'].setValue(
        this.editData.name
      );
      this.productForm.controls['email'].setValue(
        this.editData.email
      );
      this.productForm.controls['phone'].setValue(
        this.editData.phone
      );
      this.productForm.controls['age'].setValue(
        this.editData.age
      );
       
    }
  }

  addContact() {
    if(!this.editData){
      if (this.productForm.valid) {
        this.api.postContact(this.productForm.value).subscribe({
          next: (res) => {
            alert('Contact added');
            this.matDialog.close('save');
          },
          error: (err) => {
            alert('error while adding contact');
          },
        });
      }
    }else{
      this.updateContact();
    }
  }

  updateContact() {
    console.log("data to update: ", this.productForm.value);
    this.api.updateContact(this.productForm.value)
    .subscribe({
      next:(res)=>{
        alert("Updated Successfully");
        this.matDialog.close('update');
      },
      error:()=>{
        alert("error while updating");
      }
    })
  }
}
