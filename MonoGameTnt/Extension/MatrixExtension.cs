using Microsoft.Xna.Framework;

namespace ThanaNita.MonoGameTnt
{
    public static class MatrixExtension
    {
        public static Matrix Combine(this Matrix matrix1, Matrix matrix2)
        {
            return matrix2 * matrix1;
            // ตามหลัก computer graphics Normally: combine = M1 * M2;
            // กรณี MonoGame แปลกตรงลำดับมันสลับกับการรวม transform matrices ปกติ
            // สาเหตุเนื่องจาก Transform Matrix ของ MonoGame
            // จะเก็บ row กับ column สลับกับ Transform Matrix ใน Computer Graphics ปกติ
        }
    }
}
