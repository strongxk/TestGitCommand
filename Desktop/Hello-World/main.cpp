//
//  main.cpp
//  myFirstOpenGL
//
//  Created by xuke on 13-8-8.
//  Copyright (c) 2013年 xuke. All rights reserved.
//

#include <iostream>
#include <Carbon/Carbon.h>
//#include "glee.h" OpenGl Extension "autoLoader"
#include <OpenGL/gl.h>
#include <OpenGL/glu.h>
#include <Glut/glut.h>

void RenderScene(void){
    //用当前清除颜色清除窗口
    glClear(GL_COLOR_BUFFER_BIT);
    
    //把绘图颜色设置为红色
    glColor3f(1.0f,0.0f,0.0f);
    
    //用当前颜色绘制一个填充矩形
    glRectf(-25.0f,25.0f,25.0f,-25.0f);

    //刷新绘图命令
    glFlush();
}

//设置渲染状态
void SetupRC(void){
   glClearColor(0.0f,1.0f,1.0f,1.0f);
}

void ChangeSize(GLsizei w,GLsizei h){
    GLfloat aspectRatio;
    //防止被0所除
    if(h==0)
        h=1;
    
    //把视口设置为窗口的大小
    glViewport(0,0,w,h);
    
    //重置坐标系统
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    
    //建立裁剪区域(左，右，底，顶，近，远)
    aspectRatio=(GLfloat)w/(GLfloat)h;
    if(w<=h)
        glOrtho(-100.0,100.0,-100/aspectRatio,100.0/aspectRatio,1.0,-1.0);
    else
        glOrtho(-100.0*aspectRatio,100.0*aspectRatio,-100.0,100.0,1.0,-1.0);
    
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
}

//主程序入口

int main(int argc, const char * argv[])
{

    glutInit(&argc,const_cast<char **>(argv));
    glutInitDisplayMode(GLUT_SINGLE|GLUT_RGBA);
    glutCreateWindow("GLRect");
    glutDisplayFunc(RenderScene);
    glutReshapeFunc(ChangeSize);
    SetupRC();
    glutMainLoop();
    
    return 0;
}

