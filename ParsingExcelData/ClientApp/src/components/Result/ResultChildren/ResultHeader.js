import React, { Component } from 'react';

export class ResultHeader extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div>
                <div className="col-xs-6 left"></div>
                <div className="col-xs-6 right"></div>
            </div>
        );
    }
}

export default ResultHeader;