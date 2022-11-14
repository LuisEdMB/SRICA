using API.SRICA.Dominio.Entidad.US;
using API.SRICA.Dominio.Servicio.Interfaz;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.SRICA.Dominio.Servicio.Implementacion
{
    /// <summary>
    /// Implementación para el servicio de token de seguridad
    /// </summary>
    public class ServicioToken : IServicioToken
    {
        /// <summary>
        /// Método que valida las audiencias del cliente y API
        /// </summary>
        /// <param name="audienciaPermitidaCliente">Audiencia del cliente</param>
        /// <param name="audienciaPermitidaAPI">Audiencia del API</param>
        public void ValidarAudiencia(string audienciaPermitidaCliente, string audienciaPermitidaAPI)
        {
            if (!audienciaPermitidaAPI.Equals(audienciaPermitidaCliente))
                throw new Exception("La audiencia del cliente no está permitida.");
        }
        /// <summary>
        /// Método que genera el token para el usuario
        /// </summary>
        /// <param name="usuario">Usuario a quien se le generará el token</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="audienciaPermitida">Audiencia del API</param>
        /// <returns>Token generado</returns>
        public string GenerarToken(Usuario usuario, string claveSecreta, 
            string issuer, string audienciaPermitida)
        {
            var encabezadoJwt = GenerarEncabezadoJwt(claveSecreta);
            var claimSesion = GenerarClaimSesion(usuario);
            var payloadJwt = GenerarPayloadJwt(claimSesion, issuer, audienciaPermitida);
            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                encabezadoJwt, payloadJwt));
            return token;
        }
        /// <summary>
        /// Método que refresca el token del usuario
        /// </summary>
        /// <param name="tokenAnterior">Token expirado del usuario</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="audienciaPermitida">Audiencia del API</param>
        /// <returns>Token generado</returns>
        public string RefrescarToken(string tokenAnterior,
            string claveSecreta, string issuer, string audienciaPermitida)
        {
            SecurityToken tokenValidado;
            var principalClaims = GenerarClaimsDeRefresco(tokenAnterior, claveSecreta, issuer,
                audienciaPermitida, out tokenValidado);
            var tokenJwt = tokenValidado as JwtSecurityToken;
            if (tokenJwt == null || !tokenJwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
                throw new Exception("El token no tiene la encriptación correcta.");
            var encabezadoJwt = GenerarEncabezadoJwt(claveSecreta);
            var payloadJwt = GenerarPayloadJwt(principalClaims.Claims.ToList(), issuer, 
                audienciaPermitida);
            var token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                encabezadoJwt, payloadJwt));
            return token;
        }
        #region Métodos privados
        /// <summary>
        /// Método que genera el encabezado del token
        /// </summary>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <returns>Encabezado del token</returns>
        private JwtHeader GenerarEncabezadoJwt(string claveSecreta)
        {
            var clave = new SymmetricSecurityKey(
                new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(claveSecreta)));
            var credencialFirmada = new SigningCredentials(clave, 
                SecurityAlgorithms.HmacSha256);
            var encabezadoJwt = new JwtHeader(credencialFirmada);
            return encabezadoJwt;
        }
        /// <summary>
        /// Método que genera los claims del token
        /// </summary>
        /// <param name="usuario">Datos del usuario</param>
        /// <returns>Claims generados</returns>
        private List<Claim> GenerarClaimSesion(Usuario usuario)
        {
            var claimSesion = new List<Claim>();
            claimSesion.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claimSesion.Add(new Claim("Usuario", usuario.CodigoUsuario.ToString()));
            claimSesion.Add(new Claim(ClaimTypes.Role, usuario.RolUsuario.DescripcionRolUsuario.ToUpper()));
            return claimSesion;
        }
        /// <summary>
        /// Método que genera el payload del token
        /// </summary>
        /// <param name="claims">Claims del token</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="audienciaPermitida">Audiencia permitida</param>
        /// <returns>Payload generado</returns>
        private JwtPayload GenerarPayloadJwt(List<Claim> claims, string issuer, string audienciaPermitida)
        {
            return new JwtPayload(
                issuer: issuer,
                audience: audienciaPermitida,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(2)
            );
        }
        /// <summary>
        /// Método que obtiene los claims de refresco para el token
        /// </summary>
        /// <param name="tokenAnterior">Token expirado del usuario</param>
        /// <param name="claveSecreta">Clave secreta</param>
        /// <param name="issuer">Issuer</param>
        /// <param name="audienciaPermitida">Audiencia del API</param>
        /// <param name="tokenValidado">Token validado según token expirado</param>
        /// <returns>Claims de refresco para el token</returns>
        private ClaimsPrincipal GenerarClaimsDeRefresco(string tokenAnterior,
            string claveSecreta, string issuer, string audienciaPermitida, 
            out SecurityToken tokenValidado)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(tokenAnterior, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidateLifetime = false,
                ValidAudience = audienciaPermitida,
                IssuerSigningKey = new SymmetricSecurityKey(
                    new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(claveSecreta)))
            }, out tokenValidado);
            return principal;
        }
        #endregion
    }
}
