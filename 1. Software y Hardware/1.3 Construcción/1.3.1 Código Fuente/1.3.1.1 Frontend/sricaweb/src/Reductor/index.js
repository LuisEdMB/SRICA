import { combineReducers } from 'redux'
import { GeneralReducer, GeneralUsuarioLogueadoReducer } from './General'
import { DashboardReducer } from './Dashboard'
import { UsuarioReducer, UsuarioFormularioReducer, UsuarioFormularioAnteriorReducer } from './Usuario'
import { SedeEmpresaFormularioReducer, SedeEmpresaFormularioAnteriorReducer } from './SedeEmpresa'
import { AreaEmpresaReducer, AreaEmpresaFormularioReducer, AreaEmpresaFormularioAnteriorReducer } from './AreaEmpresa'
import { NomenclaturaFormularioReducer, NomenclaturaFormularioAnteriorReducer } from './Nomenclatura'
import { EquipoBiometricoReducer, EquipoBiometricoFormularioReducer, EquipoBiometricoFormularioAnteriorReducer, EquipoBiometricoAperturaPuertaReducer } from './EquipoBiometrico'
import { PersonalEmpresaReducer, PersonalEmpresaFormularioReducer, PersonalEmpresaFormularioAnteriorReducer } from './PersonalEmpresa'
import { ReporteReducer } from './Reporte'

export const AplicacionReducer = combineReducers({
    General: GeneralReducer,
    GeneralUsuarioLogueado: GeneralUsuarioLogueadoReducer,
    Dashboard: DashboardReducer,
    Usuario: UsuarioReducer,
    UsuarioFormulario: UsuarioFormularioReducer,
    UsuarioFormularioAnterior: UsuarioFormularioAnteriorReducer,
    SedeEmpresaFormulario: SedeEmpresaFormularioReducer,
    SedeEmpresaFormularioAnterior: SedeEmpresaFormularioAnteriorReducer,
    AreaEmpresa: AreaEmpresaReducer,
    AreaEmpresaFormulario: AreaEmpresaFormularioReducer,
    AreaEmpresaFormularioAnterior: AreaEmpresaFormularioAnteriorReducer,
    NomenclaturaFormulario: NomenclaturaFormularioReducer,
    NomenclaturaFormularioAnterior: NomenclaturaFormularioAnteriorReducer,
    EquipoBiometrico: EquipoBiometricoReducer,
    EquipoBiometricoFormulario: EquipoBiometricoFormularioReducer,
    EquipoBiometricoFormularioAnterior: EquipoBiometricoFormularioAnteriorReducer,
    EquipoBiometricoAperturaPuerta: EquipoBiometricoAperturaPuertaReducer,
    PersonalEmpresa: PersonalEmpresaReducer,
    PersonalEmpresaFormulario: PersonalEmpresaFormularioReducer,
    PersonalEmpresaFormularioAnterior: PersonalEmpresaFormularioAnteriorReducer,
    Reporte: ReporteReducer
})