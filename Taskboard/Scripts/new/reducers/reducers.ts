import * as types from "../actions/actionTypes";


export default function(state, action)
{
	switch(action.type)
	{
		case types.ADD_TASK:
			return state;
		default:
			return state;
	}
}