﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }
        public List<Comment> GetAllComments()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select id, subject, content, postid, userprofileid, creationdatetime
                                        from comment
                                        order by createdatetime asc";
                    var reader = cmd.ExecuteReader();

                    var comments = new List<Comment>();

                    while (reader.Read())
                    {
                        comments.Add(new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Subject = reader.GetString(reader.GetOrdinal("subject")),
                            Content = reader.GetString(reader.GetOrdinal("content")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("userprofileid")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("createdatetime")),
                            PostId = reader.GetInt32(reader.GetOrdinal("postid"))
                        });
                    }

                    reader.Close();

                    return comments;
                }
            }
        }

    

        public Comment GetCommentById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Subject, Content, UserProfileId, CreationDateTime
                                        FROM Comment WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    var reader = cmd.ExecuteReader();

                    Comment comment = null;

                    if (reader.Read())
                    {
                        comment = new Comment();
                        comment.Id = reader.GetInt32(reader.GetOrdinal("Id"));
                        comment.Subject = reader.GetString(reader.GetOrdinal("Subject"));
                        comment.Content = reader.GetString(reader.GetOrdinal("Content"));
                        comment.UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId"));
                        comment.CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"));
                    }

                    reader.Close();

                    return comment;
                }
            }
        }
        public List<Comment> GetCommentsByPostId(int postId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                SELECT c.Id, c.UserProfileId, c.PostId, c.Subject, c.Content, c.CreateDateTime, p.Id 
                FROM Comment c
                LEFT JOIN Post p ON c.PostId = p.Id
                WHERE PostId = @postId";
                    cmd.Parameters.AddWithValue("@postId", postId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Comment> comments = new List<Comment>();
                    while (reader.Read())
                    {
                        Comment comment = new Comment()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };


                        comments.Add(comment);
                    }
                    reader.Close();
                    return comments;
                }
            }
        }
        public void AddComment(Comment comment)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Comment (PostId, UserProfileId, Subject, Content, CreateDateTime)
                    OUTPUT INSERTED.ID
                    VALUES (@postId, @userProfileId, @subject, @content, @createDateTime);
                ";

                    cmd.Parameters.AddWithValue("@postId", comment.PostId);
                    cmd.Parameters.AddWithValue("@userProfileId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", comment.CreateDateTime);

                    int id = (int)cmd.ExecuteScalar();

                    comment.Id = id;
                }
            }
        }
    }
}

            //public List<Comment> GetCommentsByPostId(int postId)
            //{
            //    using (var conn = Connection)
            //    {
            //        conn.Open();
            //        using (var cmd = conn.CreateCommand())
            //        {
            //            cmd.CommandText = @"SELECT
            //                c.Id,
            //                c.PostId,
            //                c.UserProfileId,
            //                c.Subject,
            //                c.Content,
            //                c.CreateDateTime
            //            FROM Comment c
            //            JOIN Post p ON c.PostId = p.Id
            //            WHERE p.Id = @postId";

//            cmd.Parameters.AddWithValue("@postId", postId);
//            var reader = cmd.ExecuteReader();
//            var comments = new List<Comment>();

//            while (reader.Read())
//            {
//                comments.Add(NewCommentFromReader(reader));
//            }

//            reader.Close();
//            return comments;
//        }
//    }
//}

