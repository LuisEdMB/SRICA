import { createStore } from 'redux'
import { AplicacionReducer } from '../Reductor'

export const AplicacionStore = createStore(AplicacionReducer)