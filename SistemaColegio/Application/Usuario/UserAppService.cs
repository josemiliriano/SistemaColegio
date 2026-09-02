using Application.Usuario;
using Application.Usuario.DTOs;
using Domain.Entities;
using Infraestructure.Data;
using Infraestructure.Repository;
using System.Linq;

public class UserAppService : IUserAppService
{
    private readonly GeneralRepository<CDUser> _userRepository;
    private readonly GeneralRepository<Person> _personRepository;
    private readonly GeneralRepository<Role> _roleRepository;

    public UserAppService(
        GeneralRepository<CDUser> userRepository,
        GeneralRepository<Person> personRepository,
        GeneralRepository<Role> roleRepository)
    {
        _userRepository = userRepository;
        _personRepository = personRepository;
        _roleRepository = roleRepository;
    }

    public async Task<UserDto> AddUser(UserDto user)
    {
        // Validar que el nombre de usuario no exista
        var users = await _userRepository.GetAll();

        var userExists = users.Any(u =>
            u.NombreUsuario == user.NombreUsuario &&
            u.IsDelete == '0');

        if (userExists)
        {
            throw new Exception("El nombre de usuario ya existe.");
        }

        // Buscar el rol
        var roles = await _roleRepository.GetAll();

        var role = roles.FirstOrDefault(r =>
            r.NombreRol == user.NombreRol &&
            r.IsDelete == '0' &&
            r.Activo == '1');

        if (role == null)
        {
            throw new Exception("El rol especificado no existe.");
        }

        // Crear Person
        var person = new Person
        {
            Nombres = user.Nombres,
            Apellidos = user.Apellidos,
            FechaNacimiento = user.FechaNacimiento,
            Telefono = user.Telefono,
            Direccion = user.Direccion,
            Correo = user.Correo
        };

        person = await _personRepository.Add(person);

        // Crear User
        var newUser = new CDUser
        {
            IdPersona = person.IdPersona,
            IdRol = role.IdRol,
            NombreUsuario = user.NombreUsuario,
            Password = user.Password,
            Activo = user.Activo,
            IsDelete = '0'
        };

        newUser = await _userRepository.Add(newUser);

        // Retornar DTO
        return new UserDto
        {
            Nombres = person.Nombres,
            Apellidos = person.Apellidos,
            FechaNacimiento = person.FechaNacimiento,
            Telefono = person.Telefono,
            Direccion = person.Direccion,
            Correo = person.Correo,

            NombreUsuario = newUser.NombreUsuario,
            Password = newUser.Password,
            Activo = newUser.Activo,

            NombreRol = role.NombreRol
        };
    }

    public async Task<List<UserDto>> GetAllUser()
    {
        var users = await _userRepository.GetAllInclude(
            u => u.Persona,
            u => u.Rol);

        return users
            .Where(u => u.IsDelete == '0')
            .Select(u => new UserDto
            {
                Nombres = u.Persona.Nombres,
                Apellidos = u.Persona.Apellidos,
                FechaNacimiento = u.Persona.FechaNacimiento,
                Telefono = u.Persona.Telefono,
                Direccion = u.Persona.Direccion,
                Correo = u.Persona.Correo,

                NombreUsuario = u.NombreUsuario,
                Password = u.Password,
                Activo = u.Activo,

                NombreRol = u.Rol.NombreRol
            })
            .ToList();
    }

    public async Task<UserDto> GetUserById(int idUsuario)
    {
        var users = await _userRepository.GetAllInclude(
            u => u.Persona,
            u => u.Rol);

        var user = users.FirstOrDefault(u =>
            u.IdUsuario == idUsuario &&
            u.IsDelete == '0');

        if (user == null)
        {
            return null;
        }

        return new UserDto
        {
            Nombres = user.Persona.Nombres,
            Apellidos = user.Persona.Apellidos,
            FechaNacimiento = user.Persona.FechaNacimiento,
            Telefono = user.Persona.Telefono,
            Direccion = user.Persona.Direccion,
            Correo = user.Persona.Correo,

            NombreUsuario = user.NombreUsuario,
            Password = user.Password,
            Activo = user.Activo,

            NombreRol = user.Rol.NombreRol
        };
    }

    public async Task<UserDto> UpdateUser(int idUsuario, UserDto user)
    {
        var users = await _userRepository.GetAllInclude(
            u => u.Persona,
            u => u.Rol);

        var existingUser = users.FirstOrDefault(u =>
            u.IdUsuario == idUsuario &&
            u.IsDelete == '0');

        if (existingUser == null)
        {
            return null;
        }

        // Validar nombre de usuario duplicado
        var userExists = users.Any(u =>
            u.IdUsuario != idUsuario &&
            u.NombreUsuario == user.NombreUsuario &&
            u.IsDelete == '0');

        if (userExists)
        {
            throw new Exception("El nombre de usuario ya existe.");
        }

        // Buscar el nuevo rol
        var roles = await _roleRepository.GetAll();

        var role = roles.FirstOrDefault(r =>
            r.NombreRol == user.NombreRol &&
            r.IsDelete == '0' &&
            r.Activo == '1');

        if (role == null)
        {
            throw new Exception("El rol especificado no existe.");
        }

        // Actualizar Person
        existingUser.Persona.Nombres = user.Nombres;
        existingUser.Persona.Apellidos = user.Apellidos;
        existingUser.Persona.FechaNacimiento = user.FechaNacimiento;
        existingUser.Persona.Telefono = user.Telefono;
        existingUser.Persona.Direccion = user.Direccion;
        existingUser.Persona.Correo = user.Correo;

        // Actualizar User
        existingUser.NombreUsuario = user.NombreUsuario;
        existingUser.Password = user.Password;
        existingUser.Activo = user.Activo;
        existingUser.IdRol = role.IdRol;

        await _personRepository.Update(existingUser.Persona);
        await _userRepository.Update(existingUser);

        return new UserDto
        {
            Nombres = existingUser.Persona.Nombres,
            Apellidos = existingUser.Persona.Apellidos,
            FechaNacimiento = existingUser.Persona.FechaNacimiento,
            Telefono = existingUser.Persona.Telefono,
            Direccion = existingUser.Persona.Direccion,
            Correo = existingUser.Persona.Correo,

            NombreUsuario = existingUser.NombreUsuario,
            Password = existingUser.Password,
            Activo = existingUser.Activo,

            NombreRol = role.NombreRol
        };
    }

    public async Task<UserDto> DeleteUser(int idUsuario)
    {
        var user = await _userRepository.GetById(idUsuario);

        if (user == null || user.IsDelete == '1')
        {
            return null;
        }

        user.IsDelete = '1';

        await _userRepository.Delete(user);

        return new UserDto
        {
            NombreUsuario = user.NombreUsuario,
            Password = user.Password,
            Activo = user.Activo
        };
    }

    public async Task<List<UserDto>> GetUserNotDeleted()
    {
        return await GetAllUser();
    }
}