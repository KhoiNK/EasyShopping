import { Injectable } from '@angular/core';

@Injectable()
export class Base64EncodeService {
    //GetBase64(file: any): string {
    //    var reader = new FileReader();
    //    let b64: string;
    //    reader.readAsDataURL(file);
    //    reader.onload = function () {
    //        b64 = reader.result + "";
    //        console.log(reader.result);
    //    };
    //    reader.onerror = function (error) {
    //        console.log('Error: ', error);
    //    };
    //    console.log(b64);
    //    return b64;
    //}
    public base64textString: string = "";

    GetB64(file: File): string {
        this.handleFileSelect(file);
        return this.base64textString;
    }

    SetB64String(input: any) {
        this.base64textString = input;
    }

    handleFileSelect(file: File) {

        if (file) {
            var reader = new FileReader();

            reader.onload = this._handleReaderLoaded.bind(this);

            reader.readAsBinaryString(file);
        }
    }

    _handleReaderLoaded(readerEvt: any) {
        var binaryString = readerEvt.target.result;
        this.base64textString = btoa(binaryString);
        this.SetB64String(btoa(binaryString));
        console.log("base 64: " + this.base64textString);
    }
}