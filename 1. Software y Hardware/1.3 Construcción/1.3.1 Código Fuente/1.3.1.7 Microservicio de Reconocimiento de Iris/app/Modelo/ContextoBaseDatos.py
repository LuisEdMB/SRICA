from sqlalchemy import create_engine
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.orm import sessionmaker
from sqlalchemy.pool import NullPool
from ConfiguracionGlobal import ConfiguracionGlobal

configuracion = ConfiguracionGlobal()
SQLALCHEMY_DATABASE_URL = ("mysql+pymysql://" + 
    configuracion.USUARIO_BASE_DATOS + ":" + configuracion.CONTRASENA_BASE_DATOS + "@" +
    configuracion.SERVIDOR_BASE_DATOS + ":" + str(configuracion.PUERTO_BASE_DATOS) + "/" + 
    configuracion.BASE_DATOS + "?charset=utf8mb4")
engine = create_engine(SQLALCHEMY_DATABASE_URL, poolclass = NullPool)
sesion = sessionmaker(autocommit = False, autoflush = False, bind = engine)
base = declarative_base()