import React, { Component } from 'react';
import { ResultContainer } from './Result/ResultContainer';

export class Home extends Component {
    constructor(props) {
        super(props);
        this.submitFile = this.submitFile.bind(this);
        this.throwError = this.throwError.bind(this);
        this.state = {
            data: []
        };
    }

    submitFile = async(e, file) => {
        e.preventDefault();
        let thisFile = file[0];
        let form = document.getElementById("form");
        const formData = new FormData(form);
        formData.append("file", thisFile.name);
        const results = await fetch('results', {
            method: 'POST',
            body: formData
        }).then((response) => response.json());

        if (results == null) {
            this.throwError();
            return null;
        } else {
            let json = JSON.parse(results);
            this.setState({
                data: json
            });
        }
    }

    componentWillUnmount() {
        debugger;
        console.log("will unmount");
    }

    componentDidUpdate() {
        debugger;
        console.log("did update");
    }


    throwError = () => {
        //this.
        alert("File did not upload successfully, try again");
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
          <div className="Home">
            <h1>Upload Your Excel file below</h1>
            <form id="form" enctype="multipart/form-data" method="post" asp-action="Post" asp-controller="ResultsController" action="/results" onSubmit={(e) => this.isFormValid(e)}>
                <input asp-for="FileUpload.FormFile" type="file" name="file" className="col-xs-12" />
                <button type="submit">Submit</button>
              </form>
              <ResultContainer data={this.state.data} />
        </div>
    );
  }
}


export default Home;