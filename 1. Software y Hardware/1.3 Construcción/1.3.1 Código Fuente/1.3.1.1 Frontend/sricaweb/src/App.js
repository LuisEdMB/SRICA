import React from 'react';
import { Index } from './Componente'
import { AplicacionStore } from './Almacen'
import { Provider } from 'react-redux';

function App() {
  return (
    <Provider store={ AplicacionStore }>
      <Index/>
    </Provider>
  );
}

export default App;