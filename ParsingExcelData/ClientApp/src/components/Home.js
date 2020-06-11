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
        let home = document.getElementsByClassName("home-container");
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
            home[0].classList.add("hidden");
        }
    }

    componentWillUnmount() {
      //  debugger;
        console.log("will unmount");
    }

    componentDidUpdate() {
       // debugger;
        console.log("did update");
    }


    throwError = (e) => {
        e.preventDefault();
        alert("Invalid file type, try again");
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
        return (isValid === false) ? this.throwError(e) : this.submitFile(e, file);
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
          <div className="Home col-12 col-centered remove-padding">
              <div className="col-12 container remove-padding">
                  <header className="col-12">
                      <div className="row">
                          <h2 className="col-12">Use the tool on this page to convert your bills on your Excel file to visual data</h2>
                      </div>
                 </header>
                  <div className="row col-12 top-section">
                      <p className="col-12">Below is what format needs to look like</p>
                  </div>
                  <div className="file-example row">
                      <iframe className="col-6 col-centered" src="https://docs.google.com/spreadsheets/d/e/2PACX-1vSBk_wyvOMFTIbZOnd186255QRwISgKiU23glG5bpuLAkcP19xlCLJjY4kT7uu63lbSi4KpXp79LsLQ/pubhtml?widget=true&amp;headers=false"></iframe>
                      <p class="col-12"><a href="https://excelparserfiles.blob.core.windows.net/blobstorage/template-expenses.xlsx">Use Example as Template</a></p>                  
                 </div>
                 <div class="col-12">
                      <div className="home-container col-6 col-centered">
                          <h1>Upload Your Excel file below</h1>
                          <form id="form" enctype="multipart/form-data" method="post" asp-action="Post" asp-controller="ResultsController" action="/results" onSubmit={(e) => this.isFormValid(e)}>
                              <input asp-for="FileUpload.FormFile" type="file" name="file" className="col-xs-12" />
                              <button type="submit">Submit</button>
                          </form>
                      </div>
                 </div>
            </div>
            <ResultContainer data={this.state.data} />
        </div>
    );
  }
}


export default Home;