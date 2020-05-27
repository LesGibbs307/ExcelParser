import React, { Component } from 'react';

export class ResultHeader extends Component {
    constructor(props) {
        super(props);
    }

    componentWillReceiveProps(props) { // this may be depreciated
        this.setState({ data: props.data });
        debugger;
     //   credit = this.setData(credit, props);
       // debit = this.setData(data, props);
    }

    render() {
        return (
            <div className="">
                <div className="col-6 left">
                    <div className="col-12 top">
                        <p>Amount spent this year</p>
                        <h2>{this.props.debit.count}</h2>
                    </div>
                    <div className="col-12 bottom">
                        <p>Amount made this year</p>
                        <h2>{this.props.credit.count}</h2>
                    </div>                    
                </div>
                <div className="col-6 right">
                    <p>"" is the biggest issue with "" in spending</p>

                    <p>"" is the reason you made ""</p>
                </div>
            </div>
        );
    }
}

export default ResultHeader;