using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionTrabajosDeGradoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migracioninicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "especializacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__especial__3213E83FD2A8E451", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "facultad",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__facultad__3213E83F7717D40D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "escuela",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    creditos = table.Column<int>(type: "int", nullable: true),
                    estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    id_facultad = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__escuela__3213E83F8CEE3000", x => x.id);
                    table.ForeignKey(
                        name: "FK__escuela__id_facu__267ABA7A",
                        column: x => x.id_facultad,
                        principalTable: "facultad",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "linea_investigacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    id_escuela = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__linea_in__3213E83F496E2D75", x => x.id);
                    table.ForeignKey(
                        name: "FK__linea_inv__id_es__5070F446",
                        column: x => x.id_escuela,
                        principalTable: "escuela",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    contraseña = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    correo = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    fecha_ingreso = table.Column<DateTime>(type: "datetime", nullable: true),
                    imagen = table.Column<string>(type: "varchar(2500)", unicode: false, maxLength: 2500, nullable: true),
                    genero = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    fecha_ultimo_ingreso = table.Column<DateTime>(type: "datetime", nullable: true),
                    estado = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    telefono = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    direccion = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: true),
                    id_escuela = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario__3213E83F485A2C8A", x => x.id);
                    table.ForeignKey(
                        name: "FK__usuario__id_escu__2F10007B",
                        column: x => x.id_escuela,
                        principalTable: "escuela",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "asesor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    disponibilidad = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    id_usuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__asesor__3213E83FDE027B5D", x => x.id);
                    table.ForeignKey(
                        name: "FK__asesor__id_usuar__35BCFE0A",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "director",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_escuela = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__director__3213E83F77F44967", x => x.id);
                    table.ForeignKey(
                        name: "FK__director__id_esc__4316F928",
                        column: x => x.id_escuela,
                        principalTable: "escuela",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__director__id_usu__440B1D61",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "estudiante",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    matricula = table.Column<string>(type: "varchar(8)", unicode: false, maxLength: 8, nullable: true),
                    creditos_aprobados = table.Column<int>(type: "int", nullable: true),
                    id_usuario = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__estudian__3213E83F2FB2D120", x => x.id);
                    table.ForeignKey(
                        name: "FK__estudiant__id_us__3D5E1FD2",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "jurado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    disponibilidad = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__jurado__3213E83F4EEE99A0", x => x.id);
                    table.ForeignKey(
                        name: "FK__jurado__id_usuar__4AB81AF0",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usuario_escuelas",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_escuela = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario___81ECA796F7584B4B", x => new { x.id_usuario, x.id_escuela });
                    table.ForeignKey(
                        name: "FK__usuario_e__id_es__32E0915F",
                        column: x => x.id_escuela,
                        principalTable: "escuela",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__usuario_e__id_us__31EC6D26",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "asesor_especializacion",
                columns: table => new
                {
                    id_asesor = table.Column<int>(type: "int", nullable: false),
                    id_especializacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__asesor_e__032BBB8827B05D50", x => new { x.id_asesor, x.id_especializacion });
                    table.ForeignKey(
                        name: "FK__asesor_es__id_as__38996AB5",
                        column: x => x.id_asesor,
                        principalTable: "asesor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__asesor_es__id_es__398D8EEE",
                        column: x => x.id_especializacion,
                        principalTable: "especializacion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "propuestas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_trabajo = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    estado = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    id_director = table.Column<int>(type: "int", nullable: false),
                    id_investigacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__propuest__3213E83F36348942", x => x.id);
                    table.ForeignKey(
                        name: "FK__propuesta__id_di__5441852A",
                        column: x => x.id_director,
                        principalTable: "director",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__propuesta__id_in__5535A963",
                        column: x => x.id_investigacion,
                        principalTable: "linea_investigacion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "estudiante_propuesta",
                columns: table => new
                {
                    id_estudiante = table.Column<int>(type: "int", nullable: false),
                    id_propuesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__estudian__AB3AE03F73FD0E51", x => new { x.id_estudiante, x.id_propuesta });
                    table.ForeignKey(
                        name: "FK__estudiant__id_es__59063A47",
                        column: x => x.id_estudiante,
                        principalTable: "estudiante",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__estudiant__id_pr__59FA5E80",
                        column: x => x.id_propuesta,
                        principalTable: "propuestas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "trabajos_de_grado",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: false),
                    objetivo_general = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    objetivos_especificos = table.Column<string>(type: "text", nullable: false),
                    justificacion = table.Column<string>(type: "text", nullable: false),
                    progreso = table.Column<byte>(type: "tinyint", nullable: false),
                    planteamiento = table.Column<string>(type: "text", nullable: false),
                    anteproyecto = table.Column<string>(type: "varchar(2500)", unicode: false, maxLength: 2500, nullable: true),
                    trabajo = table.Column<string>(type: "varchar(2500)", unicode: false, maxLength: 2500, nullable: true),
                    estado = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    fecha_inicio = table.Column<DateTime>(type: "datetime", nullable: false),
                    fecha_fin = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_propuesta = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__trabajos__3213E83F26E602C0", x => x.id);
                    table.ForeignKey(
                        name: "FK__trabajos___id_pr__5FB337D6",
                        column: x => x.id_propuesta,
                        principalTable: "propuestas",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "asesor_trabajos",
                columns: table => new
                {
                    id_asesor = table.Column<int>(type: "int", nullable: false),
                    id_trabajo = table.Column<int>(type: "int", nullable: false),
                    rol_asesor = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__asesor_t__1731CA9B75417F54", x => new { x.id_asesor, x.id_trabajo });
                    table.ForeignKey(
                        name: "FK__asesor_tr__id_as__6FE99F9F",
                        column: x => x.id_asesor,
                        principalTable: "asesor",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__asesor_tr__id_tr__70DDC3D8",
                        column: x => x.id_trabajo,
                        principalTable: "trabajos_de_grado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "estudiante_trabajo",
                columns: table => new
                {
                    id_estudiante = table.Column<int>(type: "int", nullable: false),
                    id_trabajo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__estudian__249A903F6930AAE6", x => new { x.id_estudiante, x.id_trabajo });
                    table.ForeignKey(
                        name: "FK__estudiant__id_es__628FA481",
                        column: x => x.id_estudiante,
                        principalTable: "estudiante",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__estudiant__id_tr__6383C8BA",
                        column: x => x.id_trabajo,
                        principalTable: "trabajos_de_grado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "eventos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: false),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    id_trabajo = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__eventos__3213E83F529BFDFD", x => x.id);
                    table.ForeignKey(
                        name: "FK__eventos__id_trab__66603565",
                        column: x => x.id_trabajo,
                        principalTable: "trabajos_de_grado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__eventos__id_usua__6754599E",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "historial_de_cambios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    fecha = table.Column<DateTime>(type: "datetime", nullable: true),
                    descripcion = table.Column<string>(type: "text", nullable: true),
                    id_trabajo = table.Column<int>(type: "int", nullable: true),
                    id_propuesta = table.Column<int>(type: "int", nullable: true),
                    id_autor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__historia__3213E83F5536B92F", x => x.id);
                    table.ForeignKey(
                        name: "FK__historial__id_au__6D0D32F4",
                        column: x => x.id_autor,
                        principalTable: "usuario",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__historial__id_pr__6C190EBB",
                        column: x => x.id_propuesta,
                        principalTable: "propuestas",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__historial__id_tr__6B24EA82",
                        column: x => x.id_trabajo,
                        principalTable: "trabajos_de_grado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "jurados_trabajos",
                columns: table => new
                {
                    id_jurado = table.Column<int>(type: "int", nullable: false),
                    id_trabajo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__jurados___35CDF266FAB5383C", x => new { x.id_jurado, x.id_trabajo });
                    table.ForeignKey(
                        name: "FK__jurados_t__id_ju__73BA3083",
                        column: x => x.id_jurado,
                        principalTable: "jurado",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__jurados_t__id_tr__74AE54BC",
                        column: x => x.id_trabajo,
                        principalTable: "trabajos_de_grado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_asesor_id_usuario",
                table: "asesor",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_asesor_especializacion_id_especializacion",
                table: "asesor_especializacion",
                column: "id_especializacion");

            migrationBuilder.CreateIndex(
                name: "IX_asesor_trabajos_id_trabajo",
                table: "asesor_trabajos",
                column: "id_trabajo");

            migrationBuilder.CreateIndex(
                name: "IX_director_id_escuela",
                table: "director",
                column: "id_escuela");

            migrationBuilder.CreateIndex(
                name: "IX_director_id_usuario",
                table: "director",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_escuela_id_facultad",
                table: "escuela",
                column: "id_facultad");

            migrationBuilder.CreateIndex(
                name: "IX_estudiante_id_usuario",
                table: "estudiante",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "UQ__estudian__30962D15A64E0E26",
                table: "estudiante",
                column: "matricula",
                unique: true,
                filter: "[matricula] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_estudiante_propuesta_id_propuesta",
                table: "estudiante_propuesta",
                column: "id_propuesta");

            migrationBuilder.CreateIndex(
                name: "IX_estudiante_trabajo_id_trabajo",
                table: "estudiante_trabajo",
                column: "id_trabajo");

            migrationBuilder.CreateIndex(
                name: "IX_eventos_id_trabajo",
                table: "eventos",
                column: "id_trabajo");

            migrationBuilder.CreateIndex(
                name: "IX_eventos_id_usuario",
                table: "eventos",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_historial_de_cambios_id_autor",
                table: "historial_de_cambios",
                column: "id_autor");

            migrationBuilder.CreateIndex(
                name: "IX_historial_de_cambios_id_propuesta",
                table: "historial_de_cambios",
                column: "id_propuesta");

            migrationBuilder.CreateIndex(
                name: "IX_historial_de_cambios_id_trabajo",
                table: "historial_de_cambios",
                column: "id_trabajo");

            migrationBuilder.CreateIndex(
                name: "IX_jurado_id_usuario",
                table: "jurado",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_jurados_trabajos_id_trabajo",
                table: "jurados_trabajos",
                column: "id_trabajo");

            migrationBuilder.CreateIndex(
                name: "IX_linea_investigacion_id_escuela",
                table: "linea_investigacion",
                column: "id_escuela");

            migrationBuilder.CreateIndex(
                name: "IX_propuestas_id_director",
                table: "propuestas",
                column: "id_director");

            migrationBuilder.CreateIndex(
                name: "IX_propuestas_id_investigacion",
                table: "propuestas",
                column: "id_investigacion");

            migrationBuilder.CreateIndex(
                name: "IX_trabajos_de_grado_id_propuesta",
                table: "trabajos_de_grado",
                column: "id_propuesta");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_id_escuela",
                table: "usuario",
                column: "id_escuela");

            migrationBuilder.CreateIndex(
                name: "UQ__usuario__2A586E0B539BDA0B",
                table: "usuario",
                column: "correo",
                unique: true,
                filter: "[correo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_escuelas_id_escuela",
                table: "usuario_escuelas",
                column: "id_escuela");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asesor_especializacion");

            migrationBuilder.DropTable(
                name: "asesor_trabajos");

            migrationBuilder.DropTable(
                name: "estudiante_propuesta");

            migrationBuilder.DropTable(
                name: "estudiante_trabajo");

            migrationBuilder.DropTable(
                name: "eventos");

            migrationBuilder.DropTable(
                name: "historial_de_cambios");

            migrationBuilder.DropTable(
                name: "jurados_trabajos");

            migrationBuilder.DropTable(
                name: "usuario_escuelas");

            migrationBuilder.DropTable(
                name: "especializacion");

            migrationBuilder.DropTable(
                name: "asesor");

            migrationBuilder.DropTable(
                name: "estudiante");

            migrationBuilder.DropTable(
                name: "jurado");

            migrationBuilder.DropTable(
                name: "trabajos_de_grado");

            migrationBuilder.DropTable(
                name: "propuestas");

            migrationBuilder.DropTable(
                name: "director");

            migrationBuilder.DropTable(
                name: "linea_investigacion");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "escuela");

            migrationBuilder.DropTable(
                name: "facultad");
        }
    }
}
