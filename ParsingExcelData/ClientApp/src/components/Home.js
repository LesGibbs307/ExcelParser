import React, { Component } from 'react';

export class Home extends Component {
    constructor(props) {
        super(props);
        this.submitFile = this.submitFile.bind(this);
        this.throwError = this.throwError.bind(this);
    }

    submitFile = (e, file) => {
        e.preventDefault();
        let thisFile = file[0];
        let form = document.getElementById("form")
        const formData = new FormData(form);
        formData.append("file", thisFile.name);
        fetch('api/results', {
            method: 'POST',
            body: formData
        }).then(resp => resp.json()) // will need to check this later
    }

    throwError = () => {
        //this.
        console.log("test");
    }

    isFormValid = (e) => {
        let collection = e.nativeEvent.currentTarget.elements;
        let isValid = false;
        let file = null;
        for (let i = 0; collection.length > i; i++) {
            if (collection[i].type === "file") {
                file = collection[i].files;
                if (file.length) {
                    isValid = this.isValidFileType(file);
                }
            }
        }
        return (isValid === false) ? this.throwError() : this.submitFile(e, file);
    }

    isValidFileType = (file) => {        
        let validExtensionArr = ["csv", "xlsx"];
        let isValid = false;
        for (let i = 0; validExtensionArr.length > i; i++) {
            if (file[0].name.includes(validExtensionArr[i])) {
                isValid = true;
            }
        }
        return isValid;
    }
        
  render () {
    return (
      <div>
            <h1>Upload Your Excel file below</h1>
            <form id="form" enctype="multipart/form-data" method="post" asp-action="Post" asp-controller="ResultsController" action="/results" onSubmit={(e) => this.isFormValid(e)}>
                <input asp-for="FileUpload.FormFile" type="file" name="file" className="col-xs-12" />
                <button type="submit">Submit</button>
            </form>
      </div>
    );
  }
}
