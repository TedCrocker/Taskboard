//import $ from "jquery";
//import * as ReactDOM from "react-dom";
import * as React from "react";
import * as Redux from "redux";
import { connect } from "react-redux";
import * as Actions from "./actions/actions";
import Taskboard from "./Taskboard"

interface IAppProps
{
	value: string;
	dispatch: Redux.Dispatch<any>;
}

let store = Redux.createStore((state, action) =>
{
	switch (action.type)
	{
		default:
			return state;
	}
});


class App extends React.Component<IAppProps, {}>
{
	render()
	{

		return <Taskboard />;
	}
}


/*
//This kicks everything off
$(() =>
{
	ReactDOM.render(<Taskboard />, $('#app')[0]);
});
*/