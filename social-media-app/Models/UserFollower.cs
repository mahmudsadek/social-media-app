﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_app.Models
{

    [PrimaryKey(nameof(UserID), nameof(FollowerID))]
    public class UserFollower
    {
        [ForeignKey("User")]
        public int UserID { get; set; }

        public User User { get; set; }

        [ForeignKey("Follower")]
        public int FollowerID { get; set; }

        public User Follower { get; set; }

        public DateTime Date { get; set; }
    }
}