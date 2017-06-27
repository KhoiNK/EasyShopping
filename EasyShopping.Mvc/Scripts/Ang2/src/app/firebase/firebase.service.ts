import { Injectable } from '@angular/core';
import { AngularFireModule } from 'angularfire2';
import { AngularFireAuth } from 'angularfire2/auth';
//import { AngularFire, FirebaseListObservable } from 'firebase';
//import { AngularFireDatabase, FirebaseListObservable } from 'angularfire2/database';
import { Upload } from './Upload';
import * as firebase from 'firebase'; 

@Injectable()
export class FirebaseService {

    constructor() { }

    private basePath: string = '/uploads';
    //uploads: FirebaseListObservable;

    pushUpload(upload: Upload, storeId: number) {
        let storageRef = firebase.storage().ref();
        let uploadTask = storageRef.child(`${this.basePath}/${storeId}/${upload.file.name}`).put(upload.file);

        uploadTask.on(firebase.storage.TaskEvent.STATE_CHANGED,
            (snapshot: any) => {
                // upload in progress
                upload.progress = (snapshot.bytesTransferred / snapshot.totalBytes) * 100;
            },
            (error: any) => {
                // upload failed
                console.log(error);
            },
            () => {
                // upload success
                upload.url = uploadTask.snapshot.downloadURL;
                upload.name = upload.file.name;
                //this.saveFileData(upload)
            }
        );
    }

    private deleteFileStorage(storeId: number, name: string) {
        let storageRef = firebase.storage().ref();
        storageRef.child(`${this.basePath}/${storeId}/${name}`).delete()
    }
    // Writes the file details to the realtime db
    //private saveFileData(upload: Upload) {
    //    this.db.list(`${this.basePath}/`).push(upload);
    //}
}