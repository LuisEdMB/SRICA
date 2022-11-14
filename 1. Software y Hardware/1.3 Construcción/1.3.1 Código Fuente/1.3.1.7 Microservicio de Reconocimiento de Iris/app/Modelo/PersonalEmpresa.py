from sqlalchemy import Column, Integer, String
from sqlalchemy.types import Boolean, LargeBinary
from ContextoBaseDatos import base

class PersonalEmpresa(base):
    """
        Clase que representa a la entidad Personal de la Empresa (PE_PERSONAL_EMPRESA).
    """
    __tablename__ = "PE_PERSONAL_EMPRESA"
    __table_args__ = {'extend_existing': True}
    
    CodigoPersonal = Column("COD_PERSONAL", Integer, primary_key = True)
    ImagenIrisCodificado = Column("IMG_IRIS_CODIFICADO", LargeBinary)
    IndicadorEstado = Column("IND_ESTADO", Boolean)