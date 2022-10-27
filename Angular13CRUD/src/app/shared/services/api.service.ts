import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http : HttpClient) { }

  postContact(data : any){
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json');
    const httpOptions = {
      headers: headers
    }
    return this.http.post<any>("https://localhost:44357/api/Contact/Post/",data)
  }
  getContacts(){
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json');
    const httpOptions = {
      headers: headers
    }
    return this.http.get<any>("https://localhost:44357/api/Contact/GetList");
  }
  updateContact(data:any){
    return this.http.put<any>("https://localhost:44357/api/Contact/Put/",data)
  }

  deleteContact(id:number){
    return this.http.delete<any>("https://localhost:44357/api/Contact/Delete/"+id);
  }
}
