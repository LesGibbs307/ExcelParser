import React, { Component } from 'react';

export class ResultContainer extends Component {
    constructor(props) {
        super(props);        
        //this.state = data;
    }

    componentWillReceiveProps(props) { // this may be depreciated
        this.setState({ data: props.data })
    }

    render() {
        return (
            <div data={this.props.data}>Test</div>

        );
    }
}

export default ResultContainer;
