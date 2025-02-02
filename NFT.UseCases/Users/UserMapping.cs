﻿using NFT.Core.Entities;
using NFT.Shared.DataTransferObjects.Users;
using System.Linq.Expressions;

namespace NFT.UseCases.Users
{
    public static class UserMapping
    {
        public static Expression<Func<User, UserDto>> ToUserDto
        {
            get
            {
                return user => new UserDto
                {
                    Id = user.Id,
                    Login = user.Login,
                    //IsAuthenticated = user.IsAuthenticated //pentru viitor

                };
            }
        }
    }
}
